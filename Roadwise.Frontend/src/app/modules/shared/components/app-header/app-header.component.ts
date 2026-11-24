import { Component, inject } from '@angular/core';
import { CurrentUserService } from '../../../../core/services/auth/current-user.service';

/**
 * Zaglavlje aplikacije: logo, konvertor, valuta, nalog.
 * Koriste ga PublicLayoutComponent (/, /converter) i ClientLayoutComponent (/client/...).
 */
@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './app-header.component.html',
  styleUrl: './app-header.component.scss'
})
export class AppHeaderComponent {
  protected currentUser = inject(CurrentUserService);
}
