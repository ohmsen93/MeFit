import { useEffect, useState } from "react"
import { fetchExercises } from "../API/ExerciseAPI"
import { fetchWorkouts, patchWorkout, postWorkout, patchWorkoutExercises } from "../API/WorkoutAPI"
import ExerciseSelectionList from "../Components/Exercise/ExerciseSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { typeCompare } from "../Util/SortHelper"
import Background from "../Images/backgrounds/hd-squad-color.jpeg";

const WorkoutsOverview = props => {
    const [state, setState] = useState({
        selectedWorkout: null,
        selectedWExercise: null,
        selectedExercise: null
    })
    const [workouts, setWorkouts] = useState(null)
    const [exercises, setExercises] = useState(null)
    const [wExercises, setWExercises] = useState([])

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
        if (event.target.checked) {
            setState({...state, selectedWorkout: workout})
            setWExercises(exercises?.filter(e => workout.exercises?.includes(e.id)) || [])
        }
        else setState({...state, selectedWorkout: null})
    }
    const exerciseSelected = (event, exercise) => {
        console.log(exercise)
        if (event.target.checked) setState({...state, selectedExercise: exercise})
        else setState({...state, selectedExercise: null})
    }
    const wExerciseSelected = (event, exercise) => {
        console.log(exercise)
        if (event.target.checked) setState({...state, selectedWExercise: exercise})
        else setState({...state, selectedWExercise: null})
    }
    const workoutsSort = () => {
        const ws = [...workouts].sort(typeCompare)
        setWorkouts(ws)
    }
    const addWExercise = () => {
        if (state.selectedExercise !== null) {
            const index = wExercises.indexOf(state.selectedExercise)
            if (index === -1) // Currently only add if unique
                setWExercises([...wExercises, state.selectedExercise])
            else alert("Exercise is already in workout")
        }
        else alert("Must select a registered exercise to add")
    }
    const removeWExercise = () => {
        if (state.selectedWExercise !== null) {
            const wes = [...wExercises]
            const index = wes.indexOf(state.selectedWExercise)
            if (index > -1) {
                wes.splice(index, 1)
                setWExercises(wes)
                setState({...state, selectedWExercise: null})
            }
            else console.log("ERROR: WExercise NOT FOUND")
        }
        else alert("Must select a workout exercise to remove")
    }
    const saveWorkout = event => {
        event.preventDefault()
        // console.log(event)
        if (wExercises.length > 0) {
            if (event.nativeEvent.submitter.name === "save") { // PATCH workout
                if (state.selectedWorkout !== null) {
                    // console.log(event.target[0].value)
                    const w = {
                        id: state.selectedWorkout.id,
                        name: event.target[0].value,
                        type: event.target[1].value
                    }
                    if (w.name !== state.selectedWorkout.name || w.type !== state.selectedWorkout.type) patchWorkout(state.selectedWorkout.id, w)
                    const wes = {
                        exercises: wExercises.map(e => e.id)
                    }
                    if (wes.exercises.toString() !== state.selectedWorkout.exercises.toString()) patchWorkoutExercises(state.selectedWorkout.id, wes)

                    const nw = {
                        id: w.id,
                        name: w.name,
                        type: w.type,
                        exercises: wes.exercises
                    }
                    const index = workouts.indexOf(state.selectedWorkout)
                    if (index > -1) {
                        workouts[index] = nw
                        setState({...state, selectedWorkout: nw})
                    }
                    // const w = {
                    //     id: state.selectedWorkout.id,
                    //     name: event.target[0].value,
                    //     type: event.target[1].value,
                    //     exerciseIds: wExercises.map(e => e.id)
                    // }
                    // console.log(w)
                    // patchWorkout(state.selectedWorkout.id, w)
                    //     .then(r => {
                    //         const index = workouts.indexOf(state.selectedWorkout)
                    //         if (index > -1) {
                    //             workouts[index] = r
                    //             setState({...state, selectedWorkout: r})
                    //         }
                    //     })
                }
                else alert("Must select a workout to save")
            }
            else { // POST workout
                const w = {
                    name: event.target[0].value,
                    type: event.target[1].value,
                    exerciseIds: wExercises.map(e => e.id)
                }
                console.log(w)
                postWorkout(w)
                    .then(r => {
                        console.log(r)
                        setWorkouts([r, ...workouts])
                    })
            }
        }
        else alert("Must add at least 1 exercise to workout")
    }

    return (
        <>
        <div class="bg">
            <img src={Background} alt=""/>
        </div>
        <div id="Workouts" className="d-flex flex-column align-items-center hpx-720 p-5 contentBox">
            <div className="d-flex flex-fill wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100 workouts-item m-2 mb-0">
                    <div className="d-flex flex-column p-2 overflow-hidden">
                        Sort by: <button onClick={workoutsSort} className="btn btn-secondary border mt-2">Type</button>
                        <WorkoutSelectionList type="radio" workouts={workouts} workoutSelected={workoutSelected}/>
                    </div>
                </div>
                <div className="d-flex flex-column text-center wp-100 workouts-item m-2 mb-0 overflow-y-scroll">
                    <ExerciseSelectionList type="radio" exercises={wExercises} exerciseSelected={wExerciseSelected} k={1}/>
                    {props.contributor && 
                        // <div class="overflow-y-scroll">
                        <>
                            <div className="d-flex p-2">
                                <button onClick={removeWExercise} className="btn btn-secondary border wp-100">↓</button>
                                <button onClick={addWExercise} className="btn btn-secondary border wp-100">↑</button>
                            </div>
                            <ExerciseSelectionList type="radio" exercises={exercises} exerciseSelected={exerciseSelected} k={2}/>
                        </>
                        // </div>
                    }                
                </div>
                <div className="d-flex flex-column text-center wp-100 workouts-item m-2 mb-0 pt-2">
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
                        {state.selectedWExercise !== null &&
                        <>
                            <p>Exercise: {state.selectedWExercise.name}</p>
                            <p>Description:</p>
                            <p>{state.selectedWExercise.description}</p>
                            {state.selectedWExercise.musclegroups?.length > 0 ?
                            <>
                            <p>Musclegroups:</p>
                            {state.selectedWExercise.musclegroups?.map(mg => 
                                <p key={mg.id}>{mg.name}</p>
                            )}
                            </>
                            : <p>No musclegroups</p>
                            }
                        </>
                        }
                    </div>
                    {props.contributor &&
                        <form onSubmit={saveWorkout} key="WOForm-1" className="d-flex flex-column p-2">
                            <input type="text" defaultValue={state.selectedWorkout?.name} placeholder="Workout name" title="Letters and spaces only (between 2-20)" pattern="[A-Za-z\s]{2,20}" required/>
                            <input type="text" defaultValue={state.selectedWorkout?.type} placeholder="Workout type" title="Letters and spaces only (between 2-20)" pattern="[A-Za-z\s]{2,20}" required/>
                            <input type="submit" name="save" id="save-button-1" className="d-none"/>
                            <label htmlFor={"save-button-1"} className="btn btn-secondary border wp-100">Overwrite workout</label>
                            <input type="submit" name="create" id="create-button-1" className="d-none"/>
                            <label htmlFor={"create-button-1"} className="btn btn-secondary border wp-100">Save as new workout</label>
                        </form>
                    }
                </div>
            </div>
        </div>
        </>
    )
}

export default WorkoutsOverview