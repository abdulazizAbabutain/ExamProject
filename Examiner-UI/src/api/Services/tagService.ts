import type { Result } from "../../models/common/ResultModel";
import type  { PageModel } from "../../models/common/PagedResponse";

import type { Tag, TagDetail } from "../../models/tagsModel/Tag";
import api from "../axios";
import type { TagTimelineEntry } from "../../models/common/TimelineModel";

type tagResponemodel = Result<PageModel<Tag>>;

export const getTagsWithMeta = async ({
  PageNumber,
  PageSize,
  Search,
  ColorCategory,
  IsArchived,
}: {
  PageNumber: number;
  PageSize: number;
  Search?: string;
  ColorCategory?: string;
  IsArchived?: boolean;
}): Promise<tagResponemodel> => {
  const response = await api.get(`api/tag`, {
    params: { PageNumber, PageSize,  Search,  ColorCategory,  IsArchived },
  });

  return response.data;
};


export const getTagById = async (id: string): Promise<Result<TagDetail>> => {
  const response = await api.get(`/api/tag/${id}`);
  return response.data;
};

export const getTagTimeline = async (id: string): Promise<Result<PageModel<TagTimelineEntry>>> => {
  const response = await api.get(`/api/tag/${id}/timeline`);
  return response.data;
};