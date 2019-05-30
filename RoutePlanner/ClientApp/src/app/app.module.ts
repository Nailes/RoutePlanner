import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EntryComponent } from './components/entry/entry.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { from } from 'rxjs';
import { MenuComponent } from './components/menu/menu.component';
import { HistoryComponent } from './components/history/history.component';
import { ListComponent } from './components/list/list.component';
import { HttpService } from './_services/http.service';

const appRoutes: Routes =[
  { path: 'signin', component: RegistrationComponent},
  { path: '', component: EntryComponent},
  { path: 'menu', component: MenuComponent},
  { path: 'history', component: HistoryComponent}
]
@NgModule({
  declarations: [
    AppComponent,
    EntryComponent,
    RegistrationComponent,
    MenuComponent,
    HistoryComponent,
    ListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [HttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
