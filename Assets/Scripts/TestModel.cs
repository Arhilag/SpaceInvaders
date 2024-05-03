using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TestModel
{
    private int _seconds;

    private CancellationTokenSource _cancelTokenSource;
    
    public TestModel()
    {
        _cancelTokenSource = new CancellationTokenSource(); 
        CancellationToken token = _cancelTokenSource.Token;
        Task task = new Task(() => { Timer();}, token);
        task.Start();
    }

    private async void Timer()
    {
        while (true)
        {
            Debug.Log($"Second: {_seconds}");
            _seconds++;
            await Task.Delay(1000);

            if (!Application.isPlaying)
            {
                Debug.Log("Pause");
                _cancelTokenSource.Cancel();
                _cancelTokenSource.Dispose();
            }
        }
    }
}
