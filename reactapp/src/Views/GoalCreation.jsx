import { useEffect, useState } from "react";
import { fetchPrograms } from "../API/ProgramAPI";
import { fetchWorkouts, postWorkout } from "../API/WorkoutAPI";
import { fetchExercises } from "../API/ExerciseAPI";
import ExerciseSelectionList from "../Components/Exercise/ExerciseSelectionList";
import GoalCreationForm from "../Components/Goal/GoalCreationForm";
import ProgramSelectionList from "../Components/Program/ProgramSelectionList";
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList";
import Background from "../Images/backgrounds/hd-squad-color.jpeg";


const GoalCreation = () => {
    const [state, setState] = useState({
        tab: "program",
        selectedProgram: null,
        selectedWorkouts: [],
        selectedExercises: []
    })
    const [programs, setPrograms] = useState(null)
    const [workouts, setWorkouts] = useState(null)
    const [exercises, setExercises] = useState(null)

    useEffect(() => {
        const getPrograms = async () => {
            const ps = await fetchPrograms()
            console.log(ps)
            setPrograms(ps)
        }
        getPrograms()
    }, [])
    useEffect(() => {
        const getWorkouts = async () => {
            const ws = await fetchWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [])
    useEffect(() => {
        const getExercises = async () => {
            const es = await fetchExercises()
            console.log(es)
            setExercises(es)
        }
        getExercises()
    }, [])

    const changeTab = tab => {
        console.log(tab)
        setState({...state, tab, selectedProgram: null, selectedWorkouts: []})
    }
    const programSelected = (event, program) => {
        console.log(program)
        if (event.target.checked) setState({...state, selectedProgram: program})
        else setState({...state, selectedProgram: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkouts: [...state.selectedWorkouts, workout]})
        else setState({...state, selectedWorkouts: state.selectedWorkouts.filter(w => w.id !== workout.id)})
    }
    const exerciseSelected = (event, exercise) => {
        console.log(exercise)
        if (event.target.checked) setState({...state, selectedExercises: [...state.selectedExercises, exercise]})
        else setState({...state, selectedExercises: [state.selectedExercises.filter(e => e.id !== exercise.id)]})
    }
    const submitWorkout = async (event) => {
        event.preventDefault()
        const exerciseIds = state.selectedExercises.map(e => e.id)
        if (exerciseIds.length > 0) { // POST
            const workout = {
                name: event.target[0].value,
                type: "Custom",
                exerciseIds
            }
            const w = await postWorkout(workout)
            setWorkouts([w, ...workouts])
            console.log(w)
            
            changeTab("workout")
        }
        else { alert("Required: Select at least one exercise") }
    }

    return (
        <>
            <div class="bg">
                <img src={Background} alt=""/>
            </div>
            <div id="GoalCreationForm" className="d-flex flex-column align-items-center hpx-720 mt-5 p-5 contentBox">
                <div className="d-flex flex-fill wp-100 min-h-0">
                    <div className="d-flex flex-column text-center wp-50 p-2">

                        {/* GoalCreationForm */}
                        {/* <GoalCreationContext.Provider value={{program: state.selectedProgram, workouts: state.selectedWorkouts}}> */}
                            <GoalCreationForm program={state.selectedProgram} workouts={state.selectedWorkouts}/>
                        {/* </GoalCreationContext.Provider> */}

                    </div>
                    <div className="d-flex flex-column wp-100">
                        
                        {/* Tabs */}
                        <div  >
                            <input onChange={() => changeTab("program")} type="radio" name="tab-radio" id="program-tab" checked={state.tab === "program"} className="btn-check"/>
                            <label htmlFor="program-tab" className="btn btn-secondary border">Programs</label>
                            <input onChange={() => changeTab("workout")} type="radio" name="tab-radio" id="workout-tab" checked={state.tab === "workout"} className="btn-check"/>
                            <label htmlFor="workout-tab" className="btn btn-secondary border">Workouts</label>
                            <input onChange={() => changeTab("exercise")} type="radio" name="tab-radio" id="exercise-tab" checked={state.tab === "exercise"} className="btn-check"/>
                            <label htmlFor="exercise-tab" className="btn btn-secondary border">Create Workout</label>
                        </div>

                        {state.tab === "program" && 
                            // <GoalCreationContext.Provider value={programSelected}>
                                <ProgramSelectionList type="radio" programs={programs} programSelected={programSelected} k={1}/>
                            // </GoalCreationContext.Provider>
                        }

                        {state.tab === "workout" && 
                            // <GoalCreationContext.Provider value={workoutSelected}>
                                <WorkoutSelectionList type="checkbox" workouts={workouts} workoutSelected={workoutSelected} k={1}/>
                            // </GoalCreationContext.Provider>
                        }

                        {state.tab === "exercise" && 
                            <>
                            {/* <GoalCreationContext.Provider value={{workoutSelected, changeTab}}> */}
                                <ExerciseSelectionList type="checkbox" exercises={exercises} exerciseSelected={exerciseSelected} k={1}/>
                            {/* </GoalCreationContext.Provider> */}
                            
                            <form onSubmit={submitWorkout} className="text-center input-group p-2">
                                <input className="form-control" type="text" placeholder="Workout name" title="Letters and spaces only (between 2-20)" pattern="[A-Za-z\s]{2,20}" required/>
                                <button type="submit" className="btn btn-secondary border">Save workout</button>
                            </form>
                            </>
                        }
                    
                    </div>
                </div>
            </div>
        </>
    );
}

export default GoalCreation;