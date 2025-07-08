import type { LogLevel } from "@/common/enums/LogLevel";

export interface LogException {
  type: string;
  message: string;
  stackTrace: string;
  innerException?: LogException;
}

export interface LogEntry {
  id: string;
  timestamp: string;
  message: string;
  level: LogLevel;
  exception?: LogException;
  properties?: Record<string, string>;
}