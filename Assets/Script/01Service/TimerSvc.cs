using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class TimerSvc : MonoSingle<TimerSvc>
{
    public readonly DateTime startDateTime = new DateTime(1978, 1, 1, 0, 0, 0, 0);
    public List<TimingTask> timingTaskList;
    public ConcurrentDictionary<int, TimingTask> taskDic;

    public static TimerSvc instance;
    public override void Init()
    {
        instance = this;
        Debug.Log("��ʱ�����ʼ��...");
        taskDic = new ConcurrentDictionary<int, TimingTask>();
    }

    /// <summary>
    /// ��Ӽ�ʱ����
    /// </summary>
    /// <param name="delayMselTime">�������</param>
    public int AddTask(float delayMselTime, Action<int> fulfilCB, string taskDescribe="", Action cabcelCB = null, uint loop = 1)
    {
        int taskID = GetTaskID();
        double startTime = GetUTCMilliseconds();
        TimingTask timingTask = new TimingTask(taskID, startTime, delayMselTime, fulfilCB, taskDescribe, cabcelCB, loop);
        if (taskDic.TryAdd(taskID, timingTask))
        {
            Debug.Log("TimerSvc..�������" + taskDescribe + taskID);
        }
        else
        {
            Debug.Log("TimerSvc..�������ʧ��" + taskDescribe + taskID);
        }
        return taskID;
    }

    /// <summary>
    /// 
    /// ��Ӽ�ʱ����
    /// 
    /// </summary>
    /// <param name="delayMselTime">�������</param>
    /// <param name="fulfilCB"></param>
    /// <param name="cabcelCB"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    public int AddTask(float delayMselTime, Action fulfilCB, string taskDescribe="", Action cabcelCB = null, uint loop = 1)
    {
        int taskID = GetTaskID();
        double startTime = GetUTCMilliseconds();
        TimingTask timingTask = new TimingTask(taskID, startTime, delayMselTime, fulfilCB, taskDescribe, cabcelCB, loop);
        if (taskDic.TryAdd(taskID, timingTask))
        {
          //  Debug.Log("TimerSvc..�������" + taskDescribe + taskID + "_" + timingTask.delayTime);
        }
        else
        {
            Debug.Log("TimerSvc..�������ʧ��" + taskID);
        }
        return taskID;
    }


    public bool ReoveTask(int id)
    {
        if (taskDic.TryRemove(id, out TimingTask task))
        {
            task.cancelCB?.Invoke();
            task.cancelCB = null;
           // Debug.Log("TimerSvc..�Ƴ�����" + task.taskDescribe + id);
            return true;
        }
        else
        {
            Debug.LogWarning("TimerSvc..�Ƴ�����ʧ��" + id);
            return false;
        }
    }
    int id;
    public int GetTaskID()
    {
        lock ("LockGetID")
        {
            while (true)
            {
                id++;
                if (id == int.MaxValue)
                {
                    id = 0;
                }
                if (!taskDic.ContainsKey(id))
                {
                    return id;
                }
            }
        }
    }
    public void UpdateTask()
    {
        foreach (var task in taskDic.Values)
        {
            double utc = GetUTCMilliseconds();
            if (utc <= task.targteTime)//δ����ʱ��
            {
                continue;
            }
            task.loopIndex++;
            if (task.loop > 0)
            {
                task.loop--;
                if (task.loop == 0)
                {
                    //������ʱ����
                    FinsisTaskCB(task.taskID);
                }
                else
                {
                    task.targteTime = task.startTime + task.delayTime * (task.loopIndex + 1);
                    if (task.fulfilCB != null)
                    {
                        CallCB(task.taskID, task.fulfilCB);
                    }
                    else
                    {
                        CallCB(task.taskID, task.fulfil1CB);
                    }
                    //����ѭ��
                }
            }
            else
            {
                task.targteTime = task.startTime + task.delayTime * (task.loopIndex + 1);
                if (task.fulfilCB != null)
                {
                    CallCB(task.taskID, task.fulfilCB);
                }
                else
                {
                    CallCB(task.taskID, task.fulfil1CB);
                }
                //����ѭ��
            }
        }
    }
    public void FinsisTaskCB(int id)
    {
        if (taskDic.TryRemove(id, out TimingTask task))
        {
            if (task.fulfilCB != null)
            {
                CallCB(task.taskID, task.fulfilCB);
            }
            else
            {
                CallCB(task.taskID, task.fulfil1CB);
            }
            task.fulfilCB = null;
          //  Debug.Log("TimerSvc..�Ƴ�����" + task.taskDescribe + id + "_" + task.delayTime);
        }
        else
        {
            Debug.LogError("TimerSvc..�Ƴ�����ʧ��" + task.taskDescribe + +id);
        }
    }

    public void CallCB(int id, Action<int> cb)
    {
        cb.Invoke(id);
    }
    public void CallCB(int id, Action cb)
    {
        cb.Invoke();
    }

    public double GetUTCMilliseconds()
    {
        // TimeSpan ��ʾһ��ʱ��ļ��
        TimeSpan timeSpan = System.DateTime.UtcNow - startDateTime;
        return timeSpan.TotalMilliseconds;
    }
    public class TimingTask
    {
        /// <summary>
        /// ��ǰ�����ID
        /// </summary>
        public int taskID;
        /// <summary>
        /// ����ʱʱ��
        /// </summary>
        public float delayTime;
        /// <summary>
        /// �������ص�
        /// </summary>
        public Action<int> fulfilCB;

        /// <summary>
        /// �������ص�
        /// </summary>
        public Action fulfil1CB;
        /// <summary>
        /// ȡ������ص�
        /// </summary>
        public Action cancelCB;
        /// <summary>
        /// ѭ������
        /// </summary>
        public uint loop;
        public double startTime;
        public double targteTime;
        public ulong loopIndex;
        public string taskDescribe;
        public TimingTask(int taskID, double startTime, float delayTime, Action<int> fulfilCB, string taskDescribe, Action cabcelCB, uint loop = 1)
        {
            this.taskDescribe = taskDescribe;
            this.taskID = taskID;
            this.delayTime = delayTime;
            this.fulfilCB = fulfilCB;
            this.cancelCB = cabcelCB;
            this.loop = loop;
            this.startTime = startTime;
            targteTime = startTime + delayTime;
            loopIndex = 0;
        }

        public TimingTask(int taskID, double startTime, float delayTime, Action fulfilCB, string taskDescribe, Action cabcelCB, uint loop = 1)
        {
            this.taskID = taskID;
            this.delayTime = delayTime;
            this.fulfil1CB = fulfilCB;
            this.cancelCB = cabcelCB;
            this.loop = loop;
            this.startTime = startTime;
            targteTime = startTime + delayTime;
            loopIndex = 0;
        }


    }

    public void OnReset()
    {
        taskDic.Clear();
    }

}