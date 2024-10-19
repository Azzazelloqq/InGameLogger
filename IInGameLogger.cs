using System;

namespace InGameLogger
{
public interface IInGameLogger : IDisposable
{
	public void Log(string message);
	public void LogError(string message);
	public void LogError(Exception message);
	public void LogWarning(string message);
	public void LogException(Exception exception);
}
}