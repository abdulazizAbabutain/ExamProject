using Serilog.Core;
using Serilog.Events;

namespace Application.Commons.Extensions;

public static class LoggingLevelController
{
    public static readonly LoggingLevelSwitch LevelSwitch = new(LogEventLevel.Error);
}
