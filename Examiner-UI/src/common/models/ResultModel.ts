export interface Result<T> {
  isSuccess: boolean;
  value: T | null;
  errors: string[];
  statusCode: string;
}