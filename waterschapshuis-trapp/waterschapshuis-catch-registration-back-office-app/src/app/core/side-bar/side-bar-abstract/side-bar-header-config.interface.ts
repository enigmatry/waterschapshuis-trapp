import { HasBackConfirmDialog } from 'src/app/shared/alert/alert/has-back-confirm-dialog';

export interface ISideBarHeaderConfig {
    showExpansionButton: boolean;
    showBackButton: boolean;
    title: string;
    expanded?: boolean;
    backConfirmComponent?: HasBackConfirmDialog;
}
