import { useEffect, useState } from "react"
import { fetchExercises } from "../API/ExerciseAPI"
import { fetchWorkouts } from "../API/WorkoutAPI"
import ExerciseSelectionList from "../Components/Exercise/ExerciseSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { typeCompare } from "../Util/SortHelper"

const WorkoutsOverview = () => {
    const [state, setState] = useState({
        selectedWorkout: null,
        selectedExercise: null
    })
    const [workouts, setWorkouts] = useState(null)
    const [exercises, setExercises] = useState(null)

    useEffect(() => {
        // setWorkouts("loading")
        const getWorkouts = async () => {
            const ws = await fetchWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [])
    useEffect(() => {
        // setExercises("loading")
        const getExercises = async () => {
            const es = await fetchExercises()
            console.log(es)
            setExercises(es.reverse())
        }
        getExercises()
    }, [])

    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else setState({...state, selectedWorkout: null})
    }
    const exerciseSelected = (event, exercise) => {
        console.log(exercise)
        if (event.target.checked) setState({...state, selectedExercise: exercise})
        else setState({...state, selectedExercise: null})
    }
    const workoutsSort = () => {
        const ws = [...workouts].sort(typeCompare)
        setWorkouts(ws)
    }

    return (
        <>
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <div className="d-flex flex-fill border wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100">
                    Sort by: <button onClick={workoutsSort} className="btn btn-outline-secondary">Type</button>
                    <WorkoutSelectionList type="radio" workouts={workouts} workoutSelected={workoutSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <ExerciseSelectionList type="radio" exercises={exercises?.filter(e => state.selectedWorkout?.exercises.includes(e.id)) || []} exerciseSelected={exerciseSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <h3>Details</h3>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedWorkout !== null &&
                        <>
                            <p>Workout: {state.selectedWorkout.name}</p>
                            <p>Type: {state.selectedWorkout.type}</p>
                        </>
                        }
                    </div>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedExercise !== null &&
                        <>
                            <p>Exercise: {state.selectedExercise.name}</p>
                            {state.selectedExercise.musclegroups?.length > 0 ?
                            <>
                            <p>Musclegroups:</p>
                            {state.selectedExercise.musclegroups?.map(mg => 
                                <p key={mg.id}>{mg.name}</p>
                            )}
                            </>
                            : <p>No musclegroups</p>
                            }
                        </>
                        }
                    </div>
                </div>
            </div>
        </div>
        </>
    )
}

export default WorkoutsOverview