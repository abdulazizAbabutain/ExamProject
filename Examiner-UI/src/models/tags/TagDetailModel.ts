import { ColorCategory } from "@/common/enums/ColorCategory";

export interface TagDetailModel {
  id: string;
  name: string;
  colorHexCode: string;
  colorGroup: ColorCategory;
  creationDate: string;
  isArchived: boolean;
  versionNumber: number;
}