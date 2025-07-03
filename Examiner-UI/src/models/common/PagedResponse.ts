export interface PageModel<T> {
  data: T[];
  metaData: MetaData;
}

export interface MetaData {
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  nextPage: number;
  firstItemIndex: number;
  lastItemIndex: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  isLastPage: boolean;
}