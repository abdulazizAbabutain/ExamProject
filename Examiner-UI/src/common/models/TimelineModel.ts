export interface EntityTimelineModel {
    id: string;
    timestamp: string;
    actionType: string;
    actionBy: string;
    versionNumber: number;
    comment?: string;
}
