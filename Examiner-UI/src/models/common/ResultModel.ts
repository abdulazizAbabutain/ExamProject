export interface Result<T> {
  isSuccess: boolean;
  value: T;
  errors: string[];
  statusCode: string;
}