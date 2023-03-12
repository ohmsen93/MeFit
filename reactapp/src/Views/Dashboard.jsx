import { useState } from "react";
import { Calendar } from "react-calendar";
import 'react-calendar/dist/Calendar.css';
import { Link } from "react-router-dom";
import { DashboardContext } from "../Context/DashboardContext";

const Dashboard = () => {
  const [state, setState] = useState({
    selectedGoal: null
  })

  const goalSelected = goal => {
    console.log(goal)
    setState({...state, selectedGoal: goal})
  }

  return (
    <div className="d-flex flex-column align-items-center p-5">
      <h1>Dashboard</h1>
      <div className="d-flex wpx-960">

        {/* GoalList Component */}
        <DashboardContext.Provider value={goalSelected}>
          <div className="d-flex flex-column align-items-center flex-fill border wp-50 p-2">
            <p>Goals</p>
            <DashboardContext.Consumer>
              {goalSelected => (
                <div className="d-flex flex-column flex-fill overflow-y-scroll wp-100">
                  <button onClick={() => goalSelected({id: 1, name: "Goal A"})}>Goal A</button>
                  <button onClick={() => goalSelected({id: 2, name: "Goal B"})}>Goal B</button>
                </div>
              )}
            </DashboardContext.Consumer>
            <Link to="/goals/new">
              <button>Create goal</button>
            </Link>
          </div>
        </DashboardContext.Provider>

        {/* GoalProgress Component */}
        <DashboardContext.Provider value={state.selectedGoal}>
          <div className="d-flex flex-column align-items-center flex-fill border wp-50 p-2">
            <DashboardContext.Consumer>
              {goal => (
                <>
                <p>Progress</p>
                {goal !== null ? <Link to={"/goals/" + goal.id}>{goal.name}</Link> : "No goal selected.."}
                </>
              )}
            </DashboardContext.Consumer>
          </div>
        </DashboardContext.Provider>

        {/* GoalCalendar Component */}
        <div className="d-flex flex-column align-items-center border">
          <Calendar className={["wpx-240"]}/>
        </div>

      </div>
    </div>
  );
}

export default Dashboard;