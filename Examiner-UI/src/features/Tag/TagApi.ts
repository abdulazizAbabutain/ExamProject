import api from "@/api/axios";
import type { Result } from "@/common/models";
import type { TagDetailModel,TagFormModel } from "@/models/tags";

export const createTag = async (
    data: TagFormModel
): Promise<Result<TagDetailModel>> => {
  try {
    const response = await api.post<Result<TagDetailModel>>(
      `/api/tag`,
      { tagform: data},
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