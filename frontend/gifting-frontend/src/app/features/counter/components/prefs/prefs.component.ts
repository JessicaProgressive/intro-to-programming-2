import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectCountByCurrent } from '../../state';
import { CounterCommands } from '../../state/actions/counter-actions';

@Component({
  selector: 'app-prefs',
  templateUrl: './prefs.component.html',
  styleUrls: ['./prefs.component.css']
})
export class PrefsComponent {

  constructor(private store: Store){}

  countBy$ = this.store.select(selectCountByCurrent);

  setCountBy(by: 1 | 3 | 5){
    this.store.dispatch(CounterCommands.countby({by}));
  }
}
