import { createContext } from "react";

export const GoalCreationContext = createContext({
    programSelected: () => {},
    workoutSelected: () => {}
});