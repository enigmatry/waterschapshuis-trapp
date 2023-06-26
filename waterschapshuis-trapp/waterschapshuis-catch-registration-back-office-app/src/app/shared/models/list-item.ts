import { INamedEntityItem, NamedEntityItem, GetAreaEntitiesResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { FormControl } from '@angular/forms';

export interface IListItem {
    id?: string;
    name?: string;
}

export class ListItem implements IListItem {
    id?: string;
    name?: string;

    constructor() { }

    static fromNameEntityItem(item: INamedEntityItem): IListItem {
        const listItem = new ListItem();
        if (item) {
            listItem.id = item.id;
            listItem.name = item.name;
        }
        return listItem;
    }

    static fromNameEntityFormControl(control: FormControl): IListItem {
        if (control) {
            const value = control.value as INamedEntityItem;
            if (value) {
                return ListItem.fromNameEntityItem(value);
            }
        }
        return new ListItem();
    }

    static fromValues(id: string, name: string): IListItem {
        const listItem = new ListItem();
        listItem.id = id;
        listItem.name = name;
        return listItem;
    }

    static mapAreaEntitiesResponse(response: GetAreaEntitiesResponse): IListItem[] {
        return ListItem.mapToListItems(response.items);
    }

    static mapToListItems(items: NamedEntityItem[]): IListItem[] {
        return items ? items.map(item => ListItem.fromNameEntityItem(item)) : [];
    }
}
