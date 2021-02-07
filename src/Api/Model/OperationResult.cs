using System;

namespace Api.Model
{
    public class OperationResult<TResult>
    {
        #region Fields
        public bool Success { get; private set; }

        public TResult Result { get; private set; }

        public string NonSuccessMessage { get; private set; }

        public Exception Exception { get; private set; }
        #endregion

        #region Ctor
        private OperationResult()
        {
        }
        #endregion

        #region Methods
        public static OperationResult<TResult> CreateSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true, Result = result };
        }

        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage)
        {
            return new OperationResult<TResult> { Success = false, NonSuccessMessage = nonSuccessMessage };
        }

        public static OperationResult<TResult> CreateFailure(Exception ex)
        {
            return new OperationResult<TResult>
            {
                Success = false,
                NonSuccessMessage = String.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                Exception = ex
            };
        }
        #endregion
    }
}
