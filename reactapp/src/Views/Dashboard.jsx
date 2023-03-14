import { useState } from "react";
import { Calendar } from "react-calendar";
import 'react-calendar/dist/Calendar.css';
import { Link } from "react-router-dom";
import { DashboardContext } from "../Context/DashboardContext";

const Dashboard = () => {
  const [state, setState] = useState({
    goals: [
      {id: 1, name: "Goal A", endDate: new Date("2023-03-15T12:06:22.011Z"), workouts: [
        {id: 1, name: "Workout A", status: "completed"},
        {id: 2, name: "Workout B", status: "active"}
      ]},
      {id: 2, name: "Goal B", endDate: new Date("2023-04-01T12:06:22.011Z"), workouts: []}
    ],
    selectedGoal: null
  })

  const goalSelected = goal => {
    console.log(goal)
    setState({...state, selectedGoal: goal})
  }

  const dateDiff = (first, second) => {
    return Math.ceil((second - first) / (1000 * 60 * 60 * 24));
  }

  return (
    <div className="d-flex flex-column align-items-center p-5">
      <h1>Dashboard</h1>
      <div className="d-flex wpx-960 hpx-480">

        {/* GoalList Component */}
        <DashboardContext.Provider value={goalSelected}>
          <div className="d-flex flex-column align-items-center flex-fill border wp-50 hp-100 p-2">
            <p>Goals</p>
            <DashboardContext.Consumer>
              {goalSelected => (
                <div className="d-flex flex-column flex-fill overflow-y-scroll wp-100">
                  {state.goals.map(goal => 
                    <div key={goal.id} className="d-flex flex-column">
                      <input onChange={() => goalSelected(goal)} type="radio" name="goal-radio" id={"goal-radio-" + goal.id} className="btn-check"/>
                      <label htmlFor={"goal-radio-" + goal.id} className="btn btn-outline-secondary">{goal.name}</label>
                    </div>
                  )}
                </div>
              )}
            </DashboardContext.Consumer>
            <Link to="/goals/new">
              <button className="btn btn-outline-secondary">Create goal</button>
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
                {goal !== null ?
                  <>
                  <Link to={"/goals/" + goal.id}><p>View goal details</p></Link>
                  <p>You have {dateDiff(new Date(), goal.endDate)} day(s) to complete goal!</p>
                  <p>You have completed {goal.workouts.filter(w => w.status === "completed").length} out of {goal.workouts.length} workouts!</p>
                  </>
                : "No goal selected.."}
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