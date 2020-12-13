import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { FormsModule } from '@angular/forms';

// import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
// import { ContextMenuModule } from 'primeng/contextmenu';
// import { DialogModule } from 'primeng/dialog';
// import { DataViewModule } from 'primeng/dataview';
// import { InputTextModule } from 'primeng/inputtext';
// import { SidebarModule } from 'primeng/sidebar';
// import { ScrollPanelModule } from 'primeng/scrollpanel';
// import { ToastModule } from 'primeng/toast';
// import { ProgressSpinnerModule } from 'primeng/progressspinner';
// import { ConfirmDialogModule } from 'primeng/confirmdialog';
// import { TooltipModule } from 'primeng/tooltip';
// import { OverlayPanelModule } from 'primeng/overlaypanel';
// import { InputTextareaModule } from 'primeng/inputtextarea';
// import { TableModule } from 'primeng/table';
// import { ListboxModule } from 'primeng/listbox';
// import { ToggleButtonModule } from 'primeng/togglebutton';
// import { InputNumberModule } from 'primeng/inputnumber';

@NgModule({
  declarations: [ ],
  imports: [
    CommonModule,
    // FormsModule,
    // ButtonModule,
    // ContextMenuModule,
    // ConfirmDialogModule,
    // DialogModule,
    // DataViewModule,
    // InputTextModule,
    // InputTextareaModule,
    CardModule,
    // SidebarModule,
    // ScrollPanelModule,
    // ToastModule,
    // ProgressSpinnerModule,
    // TooltipModule,
    // OverlayPanelModule,
    // TableModule,
    // ListboxModule,
    // ToggleButtonModule,
    // InputNumberModule
  ],
  exports: [
    // FormsModule,
    // ButtonModule,
    // ContextMenuModule,
    // ConfirmDialogModule,
    // DialogModule,
    CardModule,
    // DataViewModule,
    // SidebarModule,
    // ScrollPanelModule,
    // ToastModule,
    // ProgressSpinnerModule,
    // TooltipModule,
    // OverlayPanelModule,
    // InputTextModule,
    // InputTextareaModule,
    // TableModule,
    // ListboxModule,
    // ToggleButtonModule,
    // InputNumberModule
  ]
})
export class SharedModule { }
