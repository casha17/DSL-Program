import { useEffect, useState } from "react";

/* eslint-disable no-alert, no-console */

// Will run after first initialize...
export const useMount = 
    (func: (() => void) | (() => () => void)) => useEffect(func, []);

/* eslint-enable no-alert */

/* eslint-disable no-alert, no-console */

// This will run before rendering but can't access anything outside the method as those are not initialize for func methods
export const useConstructor = 
    (constructorFunction: () => void) => useState(() => constructorFunction());

/* eslint-enable no-alert */
