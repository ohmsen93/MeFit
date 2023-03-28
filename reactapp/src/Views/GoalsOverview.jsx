import { useEffect, useState } from "react"
import { fetchGoals, fetchGoalWorkouts, patchGoalCompleted, patchGoalWorkout } from "../API/GoalAPI"
import GoalSelectionList from "../Components/Goal/GoalSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { getStatus, getStatusId } from "../Util/StatusHelper"
import Background from "../Images/backgrounds/hd-squad-color.jpeg";


const GoalsOverview = () => {
    const [state, setState] = useState({
        selectedGoal: null,
        selectedWorkout: null
    })
    const [goals, setGoals] = useState(null)
    const [workouts, setWorkouts] = useState(null)

    useEffect(() => {
        //setGoals("loading")
        const getGoals = async () => {
            const gs = await fetchGoals()
            console.log(gs)
            setGoals(gs.reverse())
        }
        getGoals()
    }, [])
    useEffect(() => {
        //setWorkouts("loading")
        const getWorkouts = async () => {
            const ws = await fetchGoalWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [])

    const goalSelected = (event, goal) => {
        console.log(goal)
        if (event.target.checked) setState({...state, selectedGoal: goal})
        else setState({...state, selectedGoal: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else console.log(workout === state.selectedWorkout)//setState({...state, selectedWorkout: null})
    }
    const workoutCompleted = async () => {
        const newGoalWorkout = {
            ...state.selectedWorkout,
            fkStatusId: getStatusId("Completed")
        }
        patchGoalWorkout(state.selectedWorkout.id, newGoalWorkout)
            .then(() => {
                const tempWorkout = workouts.find(w => w === state.selectedWorkout)
                if (tempWorkout !== null) {
                    tempWorkout.fkStatusId = getStatusId("Completed")
                    setState({...state, selectedWorkout: tempWorkout})
                }
            })
            .finally(goalCompleted)
    }
    const goalCompleted = async () => {
        console.log(!(workouts.filter(w => w.fkGoalId === state.selectedGoal.id).some(w => w.fkStatusId !== getStatusId("Completed"))))
        if (!(workouts.filter(w => w.fkGoalId === state.selectedGoal.id).some(w => w.fkStatusId !== getStatusId("Completed")))) {
            const newGoal = {
                ...state.selectedGoal,
                fkStatusId: getStatusId("Completed")
            }
            patchGoalCompleted(state.selectedGoal.id, newGoal)
                .finally(() => {
                    const tempGoal = goals.find(w => w === state.selectedGoal)
                    if (tempGoal !== null) {
                        tempGoal.fkStatusId = getStatusId("Completed")
                        setState({...state, selectedGoal: tempGoal})
                    }
                })
        }
    }

    return (
        <>
            <div class="bg">
                <img src={Background} alt=""/>
            </div>
            <div id="Goals" className="d-flex flex-column align-items-center hpx-720 p-5 mt-5 contentBox">
                <div className="d-flex flex-fill wp-100 min-h-0">
                    <div className="d-flex flex-column text-center wp-100 overflow-y-scroll">
                        <GoalSelectionList type="radio" goals={goals} goalSelected={goalSelected}/>
                    </div>
                    <div className="d-flex flex-column text-center wp-100 overflow-y-scroll">
                        <WorkoutSelectionList type="radio" workouts={workouts?.filter(w => w.fkGoalId === state.selectedGoal?.id) || []} workoutSelected={workoutSelected} k={1}/>
                    </div>
                    <div className="d-flex flex-column text-center wp-100">
                        <div className="d-flex flex-column flex-fill align-items-center justify-content-center wp-100 min-h-0 p-2">
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
        </>
    )
  }
  
  export default GoalsOverview;