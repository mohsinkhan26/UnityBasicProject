/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using System;
using System.Collections.Concurrent;

namespace MK.Common.Utilities
{
    /// <summary>
    /// Task queue. As if you want to perform actions in FIFO method without disturbing the execution flow of the old task
    /// </summary>
    public class TaskQueue
    {
        /// <summary>
        /// The Task queue.
        /// </summary>
        ConcurrentQueue<GameTask> Queue;
        /// <summary>
        /// The executing a task.
        /// </summary>
        bool executingATask = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MK.Common.Utilities.TaskQueue"/> class.
        /// </summary>
        public TaskQueue()
        {
            if (Queue == null) Queue = new ConcurrentQueue<GameTask>();
        }

        /// <summary>
        /// Adds the task in Queue and starts executing, if it's the next task to execute by FIFO method.
        /// </summary>
        /// <param name="_gameTask">Game task.</param>
        public void AddTask(GameTask _gameTask)
        {
            Queue.Enqueue(_gameTask);
            if (executingATask) { }
            else ExecuteNextTask();
        }

        /// <summary>
        /// Currents the task successful. Very useful as sometimes, might be another function is called from the current action
        /// </summary>
        void CurrentTaskSuccessful()
        {
            GameTask gameTask;
            Queue.TryDequeue(out gameTask); // removes from the Queue
            if (gameTask.successToExecute != null) gameTask.successToExecute.Invoke();
            executingATask = false;
            ExecuteNextTask();
        }

        /// <summary>
        /// Currents the task successful. Registers that the current task is finished its working
        /// </summary>
        /// <param name="_successToExecute">Success to execute.</param>
        public void CurrentTaskSuccessful(Action _successToExecute = null)
        {
            GameTask gameTask;
            Queue.TryPeek(out gameTask); // returns from the Queue, without removing it
            if (_successToExecute != null) gameTask.SuccessOfGameTask(_successToExecute);
            CurrentTaskSuccessful();
        }

        /// <summary>
        /// Currents the task failure. Very useful as sometimes, might be another function is called from the current action
        /// </summary>
        void CurrentTaskFailure()
        {
            GameTask gameTask;
            Queue.TryDequeue(out gameTask); // removes from the Queue
            if (gameTask.failureToExecute != null) gameTask.failureToExecute.Invoke();
            executingATask = false;
            ExecuteNextTask();
        }

        /// <summary>
        /// Currents the task failure. Registers that the current task is finished its working
        /// </summary>
        /// <param name="_failureToExecute">Failure to execute.</param>
        public void CurrentTaskFailure(Action _failureToExecute = null)
        {
            GameTask gameTask;
            Queue.TryPeek(out gameTask); // returns from the Queue, without removing it
            if (_failureToExecute != null) gameTask.FailureOfGameTask(_failureToExecute);
            CurrentTaskFailure();
        }

        /// <summary>
        /// Executes the next task.
        /// </summary>
        void ExecuteNextTask()
        {
            GameTask nextGameTask;
            Queue.TryPeek(out nextGameTask); // returns from the Queue, without removing it
            if (nextGameTask == null) return; // no task in the queue to execute
            executingATask = true;
            if (nextGameTask.actionToExecute != null) nextGameTask.actionToExecute.Invoke();
        }
    }

    /// <summary>
    /// Game task. General object of the Task Queue
    /// </summary>
    public class GameTask
    {
        public Action actionToExecute;
        public Action successToExecute;
        public Action failureToExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MK.Common.Utilities.GameTask"/> class.
        /// </summary>
        /// <param name="_actionToExecute">Action to execute.</param>
        /// <param name="_successToExecute">Success to execute.</param>
        /// <param name="_failureToExecute">Failure to execute.</param>
        public GameTask(Action _actionToExecute, Action _successToExecute = null, Action _failureToExecute = null)
        {
            actionToExecute = _actionToExecute;
            if (_successToExecute != null) successToExecute = _successToExecute;
            if (failureToExecute != null) failureToExecute = _failureToExecute;
        }

        /// <summary>
        /// Successes the of game task.
        /// </summary>
        /// <param name="_successToExecute">Success to execute.</param>
        public void SuccessOfGameTask(Action _successToExecute = null)
        {
            if (_successToExecute != null) successToExecute = _successToExecute;
        }

        /// <summary>
        /// Failures the of game task.
        /// </summary>
        /// <param name="_failureToExecute">Failure to execute.</param>
        public void FailureOfGameTask(Action _failureToExecute = null)
        {
            if (failureToExecute != null) failureToExecute = _failureToExecute;
        }
    }
}
