import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { AnimalType } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CatchDetails } from 'src/app/maps/models/catch-details.model';
import { getDateForWeeksAgoFromToday as getMondayWeeksAgoFromToday } from '../date-helper';

@Component({
  selector: 'app-by-catch-details',
  templateUrl: './by-catch-details.component.html',
  styleUrls: ['./by-catch-details.component.scss']
})
export class ByCatchDetailsComponent extends OnDestroyMixin {

  @Input() trapId: string;
  byCatches: Array<CatchDetails> = [];

  today: string = new Date().toISOString();
  mondaySixWeeksAgo: string = getMondayWeeksAgoFromToday(6);

  byCatchDate: string = new Date().toISOString();

  animalType: typeof AnimalType = AnimalType;

  constructor(
    private navController: NavController,
    private route: ActivatedRoute,
    private router: Router
  ) {
    super();
    this.route.queryParams
      .pipe(
        untilComponentDestroyed(this)
      )
      .subscribe(() => {
        const newbyCatches = this.router.getCurrentNavigation()?.extras?.state?.newByCatches as Array<CatchDetails>;
        if (newbyCatches) {
          newbyCatches.forEach(newByCatch => {
            const existingByCatch = this.byCatches.find(bc => bc.catchTypeId === newByCatch.catchTypeId);
            if (existingByCatch) {
              existingByCatch.number += newByCatch.number;
            } else {
              this.byCatches.push(newByCatch);
            }
          });
        }
      });
  }

  goToEdit(type: AnimalType): void {
    const routeTrap = this.trapId ? `${this.trapId}/` : '';

    this.navController.navigateForward(`traps/vangmiddel/${routeTrap}by-catch/${type}`);
  }

  removeByCatch(byCatchId: string): void {
    this.byCatches = this.byCatches.filter(c => c.id !== byCatchId);
  }

  getValues(): Array<CatchDetails> {

    this.byCatches.forEach(byCatch => byCatch.recordedOn = new Date(this.byCatchDate));

    return this.byCatches;
  }
}
