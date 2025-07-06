import type { Result } from "../../common/models/ResultModel";
import type  { PageModel } from "../../common/models/PagedResponse";

import type { TagListModel, TagDetailModel, AutoComplate } from "../../models/tags/Tag";
import api from "../axios";
import type { EntityTimelineModel } from "../../common/models/TimelineModel";

type tagResponemodel = Result<PageModel<TagListModel>>;

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


export const getTagById = async (id: string): Promise<Result<TagDetailModel>> => {
  const response = await api.get(`/api/tag/${id}`);
  return response.data;
};

export async function getTagTimeline(
  id: string,
  pageNumber: number = 1,
  pageSize: number = 10
) {
  try {
    const response = await api.get(`/api/tag/${id}/timeline`, {
      params: {
        PageNumber: pageNumber,
        PageSize: pageSize
      }
    });
    return response.data;
  } catch (err) {
    console.error(err);
    return { isSuccess: false, errors: ['Request failed'] };
  }
}



export const createTag = async (
  name: string,
  colorCode: string
): Promise<Result<TagDetailModel>> => {
  try {
    const response = await api.post<Result<TagDetailModel>>(
      `/api/tag`,
      { name, colorCode },
      {
        headers: {
          'Content-Type': 'application/json-patch+json',
        },
      }
    );
    return response.data;
  } catch (error: any) {
    if (error.response && error.response.data) {
      return error.response.data;
    }

    // fallback for unexpected error shape
    return {
      isSuccess: false,
      errors: ['Unexpected error occurred.'],
      value: null,
      statusCode: 'InternalServerError',
    };
  }
};


export async function autocompleteTags(query: string): Promise<AutoComplate[]> {
  try {
    const response = await api.get(`/api/tag/autocomplete?name=${encodeURIComponent(query)}`);
    if (response.data.isSuccess) {
      return response.data.value;
    }
    return [];
  } catch (error) {
    console.error(error);
    return [];
  }
}

export async function archiveTag(id: string) {
  return await api.post(`/api/tag/${id}/archive`);
}

export async function unarchiveTag(id: string) {
  return await api.post(`/api/tag/${id}/unarchive`);
}



export interface ModifiedProperty {
  propertyName: string;
  oldValue?: any;
  newValue?: any;
  propertyType: string;
}

export interface TimelineDetail {
  id: string;
  timestamp: string;
  actionType: string;
  actionBy: string;
  modifiedProperties: ModifiedProperty[];
  versionNumber: number;
}

export async function getTagTimelineDetail(tagId: string, timelineId: string) {
  try {
    const res = await api.get(`/api/tag/${tagId}/timeline/${timelineId}`);
    return res.data;
  } catch (err) {
    console.error(err);
    return { isSuccess: false, errors: ['Failed to load timeline detail.'] };
  }
}
