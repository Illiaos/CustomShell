using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


public class ThreadPool
{

    #region
    private const int MAX_VALUE = 6;
    private const int MIN_VALUE = 2;
    private const int MIN_WAIT_TIME = 10;
    private const int MAX_WAIT_TIME = 15000;
    private const int CLEAN_DATA_TIME = 60000;
    private const int SCHEDULE_TIME = 10;
    #endregion
    private Thread thread;
    public static readonly ThreadPool Instance = new ThreadPool();
    private Queue<Taskandler> StoreTasks = null;
    private Queue<Taskandler> ReadyTasks = null;
    private Thread mainThread;
    public ThreadPool()
    {
        StoreTasks = new Queue<Taskandler>();
        ReadyTasks = new Queue<Taskandler>();
        ExecuteTask();
    }
    private void ExecuteTask()
    {
        mainThread = new Thread(() =>
        {
            do
            {
                if (ReadyTasks.Count <= MAX_VALUE)
                {
                    for (int i = ReadyTasks.Count; i < MAX_VALUE; i++)
                    {
                        if (StoreTasks.Count != 0)
                        {
                            ReadyTasks.Enqueue(StoreTasks.Peek());
                            StoreTasks.Dequeue();
                        }
                    }
                }
                if (ReadyTasks.Count > 0)
                {
                    Taskandler executeHandler = ReadyTasks.Peek();
                    UserTask u = executeHandler.UserTask;
                    thread = new Thread(new ThreadStart(u));
                    thread.Start();
                    ReadyTasks.Dequeue();
                    thread.Join();
                    CommandState.Command commamd = CommandState.Instance.GetCommand();
                }
            } while (true);
        });
        mainThread.Start();
    }

    public delegate void UserTask();
    public void AddTask(UserTask task)
    {
        Taskandler taskandler = new Taskandler();
        taskandler.UserTask = task;
        StoreTasks.Enqueue(taskandler);
    }
    public bool EmptyThreadList()
    {
        if(thread==null)
        {
            return true;
        }
        if(thread.IsAlive)
        {
            return false;
        }
        else
        {
            Thread.Sleep(10);
            return true;
        }
    }
    private struct Taskandler
    {
        public UserTask UserTask;
    }
}
