using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlTransientErrorHandler
{
    public class TransientErrorHandler
    {
        private bool _ShouldIncrementTimeout = true;

        private int _NumberOfAttempts = 3;

        public TransientErrorHandler WithAttempts(int numberOfAttempts = 3)
        {
            if (numberOfAttempts <= 0)
                numberOfAttempts = 3;

            _NumberOfAttempts = numberOfAttempts;

            return this;
        }

        public TransientErrorHandler WithIncrementalTimeout(bool shouldIncrement = true)
        {
            _ShouldIncrementTimeout = shouldIncrement;

            return this;
        }

        public void Execute(Action action)
        {
            bool shouldRetry = false;

            int counter = 0;
            int waiter = 1;

            do
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    shouldRetry = SqlAzureRetriableExceptionDetector.ShouldRetryOn(ex);
                    if (shouldRetry == false) throw;

                    counter++;

                    Thread.Sleep(100 * counter * waiter);

                    if (_ShouldIncrementTimeout)
                    {
                        waiter += 5;
                    }

                }
            } while (shouldRetry && counter < _NumberOfAttempts);

            throw new Exception("Failed to connect to Database");
        }

        public T Execute<T>(Func<T> action)
        {
            bool shouldRetry = false;

            int counter = 0;
            int waiter = 1;

            do
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    shouldRetry = SqlAzureRetriableExceptionDetector.ShouldRetryOn(ex);
                    if (shouldRetry == false) throw;

                    counter++;

                    Thread.Sleep(100 * counter * waiter);

                    if (_ShouldIncrementTimeout)
                    {
                        waiter += 5;
                    }
                }
            } while (shouldRetry && counter < _NumberOfAttempts);

            throw new Exception("Failed to connect to Database");
        }
    }
}
