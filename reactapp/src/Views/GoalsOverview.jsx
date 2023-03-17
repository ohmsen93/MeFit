import { useEffect, useState } from "react"
import { fetchGoals } from "../API/GoalAPI"
import { fetchWorkouts, patchWorkout } from "../API/WorkoutAPI"
import GoalSelectionList from "../Components/Goal/GoalSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { getStatus } from "../Util/StatusHelper"

const GoalsOverview = () => {
    const [state, setState] = useState({
        selectedGoal: null,
        selectedWorkout: null
    })
    const [goals, setGoals] = useState("loading")
    const [workouts, setWorkouts] = useState("loading")

    useEffect(() => {
        setGoals("loading")
        const getGoals = async () => {
            const gs = await fetchGoals()
            console.log(gs)
            setGoals(gs.reverse())
        }
        getGoals()
    }, [])
    useEffect(() => {
        const getWorkouts = async () => {
            const ws = await fetchWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [state.selectedGoal])

    const goalSelected = (event, goal) => {
        console.log(goal)
        if (event.target.checked) setState({...state, selectedGoal: goal})
        else setState({...state, selectedGoal: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else setState({...state, selectedWorkout: null})
    }
    const workoutCompleted = async () => {
        const newWorkout = {
            fkStatusId: 2
        }
        patchWorkout(state.selectedWorkout.id, newWorkout)
    }

    return (
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <div className="d-flex flex-fill border wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100">
                    <GoalSelectionList type="radio" goals={goals} goalSelected={goalSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <WorkoutSelectionList type="radio" workouts={workouts/*state.selectedGoal.workouts*/} workoutSelected={workoutSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <div className="d-flex flex-column flex-fill align-items-center justify-content-center border wp-100 min-h-0 p-2">
                        <h3>Details</h3>
                        <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedGoal !== null &&
                        <>
                            <p>Goal: Goal {state.selectedGoal.id}</p>
                            <p>Period: {state.selectedGoal.startDate.split("T")[0]} - {state.selectedGoal.endDate.split("T")[0]}</p>
                            <p>Status: {getStatus(state.selectedGoal.fkStatusId)}</p>
                            <p>Program: {state.selectedGoal.programNavn ?? "Custom"}</p>
                        </>
                        }
                        </div>
                        <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedWorkout !== null &&
                        <>
                            <p>Workout: {state.selectedWorkout.name}</p>
                            <p>Status: {getStatus(state.selectedWorkout.fkStatusId)}</p>
                            <button onClick={workoutCompleted} className="btn btn-outline-secondary">Log workout as completed</button>
                        </>
                        }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
  }
  
  export default GoalsOverview;