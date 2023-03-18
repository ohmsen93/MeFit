import { useEffect, useState } from "react";
import { Calendar } from "react-calendar";
import 'react-calendar/dist/Calendar.css';
import { Link } from "react-router-dom";
import { fetchGoals } from "../API/GoalAPI";
import GoalSelectionList from "../Components/Goal/GoalSelectionList";
import { getStatusId } from "../Util/StatusHelper";

const Dashboard = () => {
  const [state, setState] = useState({
    selectedGoal: null
  })
  const [goals, setGoals] = useState("loading")

  useEffect(() => {
    setGoals("loading")
    const getGoals = async () => {
        const gs = await fetchGoals()
        console.log(gs)
        setGoals(gs.reverse())
    }
    getGoals()
  }, [])

  const goalSelected = (event, goal) => {
    console.log(goal)
    if (event.target.checked) setState({...state, selectedGoal: goal})
    else setState({...state, selectedGoal: null})
  }

  const dateDiff = (first, second) => {
    return Math.ceil((second - first) / (1000 * 60 * 60 * 24));
  }

  return (
    <div className="d-flex flex-column align-items-center p-5">
      <div className="d-flex wpx-960 hpx-480">

        {/* GoalList Component */}
        {/* <DashboardContext.Provider value={goalSelected}> */}
          <div className="d-flex flex-column align-items-center flex-fill border wp-50 hp-100">
            
            <GoalSelectionList type="radio" goals={goals} goalSelected={goalSelected}/>
            {/* <p>Goals</p>
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
            </DashboardContext.Consumer> */}

            <div>
              <Link to="/goals">
                <button className="btn btn-outline-secondary">View all goals</button>
              </Link>
              <Link to="/goals/new">
                <button className="btn btn-outline-secondary">Create new goal</button>
              </Link>
            </div>
          </div>
        {/* </DashboardContext.Provider> */}

        {/* GoalProgress Component */}
        {/* <DashboardContext.Provider value={state.selectedGoal}> */}
          <div className="d-flex flex-column align-items-center flex-fill border wp-50 p-2">
            {/* <DashboardContext.Consumer> */}
              {/* {goal => (
                <> */}
                <h3>Progress</h3>
                {state.selectedGoal !== null ?
                  <>
                  <p>Goal {state.selectedGoal.id}</p>
                  <p>You have {dateDiff(new Date(), new Date(state.selectedGoal.endDate))} day(s) to complete goal!</p>
                  <p>You have completed {state.selectedGoal.workouts.filter(w => w.fkStatusId === getStatusId("Completed")).length} out of {state.selectedGoal.workouts.length} workouts!</p>
                  <Link to="/goals">View more details</Link>
                  </>
                : "No goal selected.."}
                {/* </>
              )} */}
            {/* </DashboardContext.Consumer> */}
          </div>
        {/* </DashboardContext.Provider> */}

        {/* GoalCalendar Component */}
        <div className="d-flex flex-column align-items-center border">
          <Calendar className={["wpx-240"]}/>
        </div>

      </div>
    </div>
  );
}

export default Dashboard;