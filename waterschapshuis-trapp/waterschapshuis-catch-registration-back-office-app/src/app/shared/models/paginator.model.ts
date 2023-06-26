import { MatPaginatorIntl } from '@angular/material/paginator';
import { Injectable } from '@angular/core';

@Injectable()
export class CustomMatPaginator extends MatPaginatorIntl {
  itemsPerPageLabel = 'Items per pagina';
}
