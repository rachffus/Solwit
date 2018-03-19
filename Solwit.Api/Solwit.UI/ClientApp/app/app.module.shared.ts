import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { LogDataComponent } from './components/logdata/logdata.component';

@NgModule({
    declarations: [
        AppComponent,
        LogDataComponent,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
          { path: '', redirectTo: 'log-data', pathMatch: 'full' },
            { path: 'log-data', component: LogDataComponent },
            { path: '**', redirectTo: 'log-data' }
        ])
    ]
})
export class AppModuleShared {
}
