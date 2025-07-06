import type { PageModel } from "../../common/models/PagedResponse";
import type { LogEntry } from "../../models/logModel";
import type { LogLevel } from "@/common/enums/LogLevel";
import api from "../axios";

export async function getLogs(page: number = 1, pageSize: number = 50, level: LogLevel | null, ): Promise<PageModel<LogEntry> | null> {
  try {
    const res = await api.get(`/api/system/logs`, {
      params: { pageNumber: page, pageSize }
    });
    return res.data;
  } catch (err) {
    console.error(err);
    return null;
  }
}
