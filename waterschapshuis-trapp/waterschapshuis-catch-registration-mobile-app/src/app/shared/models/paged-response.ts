import { Observable, EMPTY } from 'rxjs';
import { expand, reduce } from 'rxjs/operators';

export interface IPagedResponse<T> {
  items?: T[];
  itemsTotalCount?: number;
  currentPage?: number;
  pageSize?: number;
  totalPages?: number;
  hasNextPage?: boolean;
}
