export interface Tag {
  id: string;
  name: string;
  colorCode: string;
  colorCategory: string;
}

export interface TagDetail {
  id: string;
  name: string;
  colorHexCode: string;
  colorGroup: string;
  creationDate: string;
  isArchived: boolean;
  versionNumber: number;
}

