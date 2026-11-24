import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { MyProfileComponent } from '../account/my-profile/my-profile.component';
import { AdminSettingsComponent } from './admin-settings/admin-settings.component';
import { ExchangeRatesComponent } from './finance/exchange-rates/exchange-rates.component';

// Rute ispod /admin (guard u app-routing-module.ts: samo administrator).
// Nove stranice za Roadwise (kandidati, termini, testovi...) dodaju se kao children.
const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      // POČETNA (/admin) – mjesto za dashboard
      {
        path: '',
        component: AdminHomeComponent,
        pathMatch: 'full',
      },

      // FINANCE
      {
        path: 'exchange-rates',
        component: ExchangeRatesComponent,
      },

      // POSTAVKE APLIKACIJE (prazna stranica – ovdje idu postavke aplikacije)
      {
        path: 'settings',
        component: AdminSettingsComponent,
      },

      // MOJ PROFIL (ista stranica kao /client/profile, deklarisana u AccountModule)
      {
        path: 'profile',
        component: MyProfileComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
