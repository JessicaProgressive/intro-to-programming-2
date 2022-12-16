import { Injectable } from '@angular/core';
import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, filter, map, of, tap } from 'rxjs';
import { selectCountData } from '..';
import { CounterCommands, CounterDocuments } from '../actions/counter-actions';
import { CountState, initialState } from '../reducers/count-reducer';
import { z } from 'zod';
import { ApplicationEvents } from 'src/app/state/actions/app-actions';
@Injectable()
export class CounterDataEffects {
  private readonly COUNT_DATA_KEY = 'count-data';
  private readonly CountDataSchema = z.object({
    current: z.number(),
    by: z.union([
      z.literal(1),
      z.literal(3),
      z.literal(5),
    ]),
  });

  // CounterCommandsLoad => ?? => CounterDocuments.counter
  loadCountData$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(CounterCommands.load), // we only care about this action
        map(() => localStorage.getItem(this.COUNT_DATA_KEY)), // check local storage, this going to be null | "string"
        filter((storedStuff) => !!storedStuff), // probably better with storedStuff => storedStuff !== null
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        map((storedString) => JSON.parse(storedString!)), // just making the compiler happy.
        map((obj) => this.CountDataSchema.parse(obj) as CountState),
        map((payload) => CounterDocuments.counter({ payload })),
        catchError((err) =>
          of(
            ApplicationEvents.error({
              source: 'counter',
              message: 'We have a 1337 Hax0r',
              payload: err,
            }),
          ),
        ),
      );
    },
    { dispatch: true },
  );

  saveCountData$ = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(
          CounterCommands.countby,
          CounterCommands.decremented,
          CounterCommands.incremented,
          CounterCommands.reset,
        ), // stop here if it isn't one of these.
        concatLatestFrom(() => this.store.select(selectCountData)), // => subscribed observable of our data returned from selectCountData
        // prettier-ignore
        map(([,data]) => JSON.stringify(data),
        ), // turn that data into a string so I can write it local storage
        tap((data) => localStorage.setItem(this.COUNT_DATA_KEY, data)), // write that sucker to localstorage
      );
    },
    { dispatch: false },
  ); // whatever emerges here has to be action, and it is sent to the store.

  constructor(private actions$: Actions, private store: Store) {}
}