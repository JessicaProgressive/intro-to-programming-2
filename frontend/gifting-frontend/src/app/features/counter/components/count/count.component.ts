import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectCountCurrent } from '../../state';
import { CounterCommands } from '../../state/actions/counter-actions';

@Component({
  selector: 'app-count',
  templateUrl: './count.component.html',
  styleUrls: ['./count.component.css']
})
export class CountComponent {

  constructor(private store: Store){}

  current$ = this.store.select(selectCountCurrent);

  increment(){
    this.store.dispatch(CounterCommands.incremented());
  }

  decrement(){
    this.store.dispatch(CounterCommands.decremented());
  }

  reset(){
    this.store.dispatch(CounterCommands.reset());
  }
}
