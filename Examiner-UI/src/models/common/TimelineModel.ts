export interface TagTimelineEntry {
    id: string;
    timestamp: string;
    actionType: string;
    actionBy: string;
    versionNumber: number;
    comment?: string;
}
