export function logInfo(message: string, data?: any) {
  console.info(`[INFO] ${message}`, data);
}

export function logError(message: string, data?: any) {
  console.error(`[ERROR] ${message}`, data);
}

export function logDebug(message: string, data?: any) {
  if (import.meta.env.DEV) {
    console.debug(`[DEBUG] ${message}`, data);
  }
}
