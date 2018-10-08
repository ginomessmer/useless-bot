import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { QuotesComponent } from './pages/quotes/quotes.component';
import { AuthGuard } from './guards/auth.guard';

import { HttpModule } from '@angular/http';

const routes: Routes = [
  {
    path: 'quotes',
    component: QuotesComponent
  },
  {
    path: '**',
    redirectTo: '',
    canActivate: [AuthGuard]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    QuotesComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes,
    {
      enableTracing: true
    }),
    FormsModule,
    NgbModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
