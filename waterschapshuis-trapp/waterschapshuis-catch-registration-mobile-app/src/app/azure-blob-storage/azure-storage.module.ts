import { NgModule } from '@angular/core';
import { azureBlobStorageFactory, BLOB_STORAGE_TOKEN } from './services/token';

@NgModule({
  imports: [],
  declarations: [
  ],
  providers: [
    {
      provide: BLOB_STORAGE_TOKEN,
      useFactory: azureBlobStorageFactory
    }
  ]
})
export class AzureStorageModule { }
