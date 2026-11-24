import { Component, inject } from '@angular/core';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';

/**
 * Početna (landing) stranica javnog dijela aplikacije.
 * Ruta: '' unutar PublicLayoutComponent (vidi public-routing-module.ts).
 */
@Component({
  selector: 'app-landing',
  standalone: false,
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.scss',
})
export class LandingComponent {
  protected currentUser = inject(CurrentUserService);

  protected readonly features = [
    { icon: 'directions_car', key: 'LANDING.FEATURE_LESSONS' },
    { icon: 'quiz', key: 'LANDING.FEATURE_TESTS' },
    { icon: 'menu_book', key: 'LANDING.FEATURE_MATERIALS' },
  ];
}
