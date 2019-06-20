using System;

namespace D3vS1m.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorViewModel()
        {
            StatusCode = 500;
        }

        // -- properties

        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// The Http status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The message phrase from the status code
        /// </summary>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Optional exception object
        /// </summary>
        public Exception Exception { get; set; }
    }
}