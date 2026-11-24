import { Component, inject } from '@angular/core';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';

/**
 * Početna stranica admin panela (ruta /admin).
 * Ovdje ide dashboard (tačka 10 uputa: https://ui-patterns.com/patterns/dashboard).
 */
@Component({
  selector: 'app-admin-home',
  standalone: false,
  templateUrl: './admin-home.component.html',
  styleUrl: './admin-home.component.scss',
})
export class AdminHomeComponent {
  protected currentUser = inject(CurrentUserService).currentUser;
}
