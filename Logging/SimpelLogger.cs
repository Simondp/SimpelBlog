using System;
using NLog;

namespace SimpelBlog.Logging
{
    public class SimpelLogger : ISimpelLogger
    {
	private readonly NLog.Logger _logger;    
	public SimpelLogger()
	{
		_logger= NLog.LogManager.GetCurrentClassLogger();

	}  
        public void LogDebug(string message)
        {
	    if(message == null)
	    {
	    	throw new ArgumentNullException("Message can't be null");
	    }	
	    _logger.Log(LogLevel.Debug,message);
         }

        public void LogError( string message, Exception e)
        {
		
	    if(message == null)
	    {
	    	throw new ArgumentNullException("Message can't be null");
	    }	
	    _logger.Error(e,message);
        }

        public void LogFatal( string message, Exception e)
        {
	    if(message == null)
	    {
	    	throw new ArgumentNullException("Message can't be null");
	    }	
	    _logger.Fatal(e,message);
        }

        public void LogInfo( string message)
        {
	    if(message == null)
	    {
	    	throw new ArgumentNullException("Message can't be null");
	    }	
	    _logger.Info(message);
        }

        public void LogTrace(string message)
        {
	    _logger.Trace(message);
        }
	public void LogStop(){
		NLog.LogManager.Shutdown();
	}
    }
}
