import { createContext } from "react";

export const DashboardContext = createContext({
    selectedGoal: {},
    goalSelected: () => {}
});