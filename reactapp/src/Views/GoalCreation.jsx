import { useEffect, useState } from "react";
import { fetchPrograms } from "../API/ProgramAPI";
import { postWorkout } from "../API/WorkoutAPI";
import GoalCreationForm from "../Components/Goal/GoalCreationForm";
import { GoalCreationContext } from "../Context/GoalCreationContext";

const GoalCreation = () => {
    const [state, setState] = useState({
        tab: "program",
        selectedProgram: null,
        selectedWorkouts: [],
        selectedExercises: []
    })
    const [programs, setPrograms] = useState([])

    useEffect(() => {
        const getPrograms = async () => {
            setPrograms(await fetchPrograms())
        }
        getPrograms()
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
        else setState({...state, selectedExercises: state.selectedExercises.filter(e => e.id !== exercise.id)})
    }
    const submitWorkout = (event) => {
        event.preventDefault()
        const exerciseIds = state.selectedExercises.map(e => e.id)
        if (exerciseIds.length > 0) { // POST
            let workout = {
                name: event.target[0].value,
                type: "Custom",
                fkUserProfileId: 1,
                exerciseIds
            }
            postWorkout(workout)
            console.log(workout)
            changeTab("workout")
        }
        else { alert("Required: Select at least one exercise") }
    }

    return (
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <h1>New goal</h1>
            <div className="d-flex flex-fill border wp-100">
                <div className="d-flex flex-column text-center wp-50 p-2">

                    {/* GoalCreationForm */}
                    <GoalCreationContext.Provider value={{program: state.selectedProgram, workouts: state.selectedWorkouts}}>
                        <GoalCreationForm/>
                    </GoalCreationContext.Provider>

                </div>
                <div className="d-flex flex-column wp-100">
                    
                    {/* Tabs */}
                    <div>
                        <input onChange={() => changeTab("program")} type="radio" name="tab-radio" id="program-tab" checked={state.tab === "program"} className="btn-check"/>
                        <label htmlFor="program-tab" className="btn btn-outline-secondary">Programs</label>
                        <input onChange={() => changeTab("workout")} type="radio" name="tab-radio" id="workout-tab" checked={state.tab === "workout"} className="btn-check"/>
                        <label htmlFor="workout-tab" className="btn btn-outline-secondary">Workouts</label>
                        <input onChange={() => changeTab("exercise")} type="radio" name="tab-radio" id="exercise-tab" checked={state.tab === "exercise"} className="btn-check"/>
                        <label htmlFor="exercise-tab" className="btn btn-outline-secondary">Create Workout</label>
                    </div>

                    {state.tab === "program" && 
                        <GoalCreationContext.Provider value={{programs, programSelected}}>
                        {/* ProgramSelectionList Component */}
                        <div className="d-flex flex-column flex-fill align-items-center border wp-100 p-2">
                            <p>Choose a program:</p>
                            <GoalCreationContext.Consumer>
                                {({programs, programSelected}) => (
                                    <div className="d-flex flex-column text-center flex-fill text-center overflow-y-scroll wp-100">
                                        {programs.map(p => 
                                            <div className="d-flex flex-column" key={p.id}>
                                                <input onChange={e => programSelected(e, p)} type="radio" name="program-list-radio" id={`program-radio-${p.id}`} className="btn-check"/>
                                                <label htmlFor={`program-radio-${p.id}`} className="btn btn-outline-secondary">{p.name}</label>
                                            </div>
                                        )}
                                        {/* <input onChange={e => programSelected(e, {id: 1, name: "Program A"})} type="radio" name="program-list-radio" id="program-radio-1" className="btn-check"/>
                                        <label htmlFor="program-radio-1" className="btn btn-outline-secondary">Program A</label>
                                        <input onChange={e => programSelected(e, {id: 2, name: "Program B"})} type="radio" name="program-list-radio" id="program-radio-2" className="btn-check"/>
                                        <label htmlFor="program-radio-2" className="btn btn-outline-secondary">Program B</label> */}
                                    </div>
                                )}
                            </GoalCreationContext.Consumer>
                        </div>
                        </GoalCreationContext.Provider>
                    }

                    {state.tab === "workout" && 
                        <GoalCreationContext.Provider value={workoutSelected}>
                            {/* WorkoutSelectionList Component */}
                            <div className="d-flex flex-column flex-fill align-items-center border wp-100 p-2">
                                <p>Choose workouts:</p>
                                <GoalCreationContext.Consumer>
                                    {workoutSelected => (
                                        <div className="d-flex flex-column text-center flex-fill overflow-y-scroll wp-100">
                                            <input onChange={e => workoutSelected(e, {id: 1, name: "Workout A"})} type="checkbox" name="workout-list-checkbox" id="workout-checkbox-1" className="btn-check"/>
                                            <label htmlFor="workout-checkbox-1" className="btn btn-outline-secondary">Workout A</label>
                                            <input onChange={e => workoutSelected(e, {id: 2, name: "Workout B"})} type="checkbox" name="workout-list-checkbox" id="workout-checkbox-2" className="btn-check"/>
                                            <label htmlFor="workout-checkbox-2" className="btn btn-outline-secondary">Workout B</label>
                                        </div>
                                    )}
                                </GoalCreationContext.Consumer>
                            </div>
                        </GoalCreationContext.Provider>
                    }

                    {state.tab === "exercise" && 
                        /* ExerciseSelectionList Component */
                        <div className="d-flex flex-column flex-fill align-items-center border wp-100 p-2">
                            <p>Choose exercises:</p>
                            <div className="d-flex flex-column text-center flex-fill overflow-y-scroll wp-100">
                                <input onChange={e => exerciseSelected(e, {id: 1, name: "Exercise A"})} type="checkbox" name="exercise-list-checkbox" id="exercise-checkbox-1" className="btn-check"/>
                                <label htmlFor="exercise-checkbox-1" className="btn btn-outline-secondary">Exercise A</label>
                                <input onChange={e => exerciseSelected(e, {id: 2, name: "Exercise B"})} type="checkbox" name="exercise-list-checkbox" id="exercise-checkbox-2" className="btn-check"/>
                                <label htmlFor="exercise-checkbox-2" className="btn btn-outline-secondary">Exercise B</label>
                            </div>
                            <form onSubmit={submitWorkout}>
                                <input type="text" placeholder="Workout name" title="Letters and spaces only (between 2-20)" pattern="[A-Za-z\s]{2,20}" required/>
                                <button type="submit" className="btn btn-outline-secondary">Save workout</button>
                            </form>
                        </div>
                    }
                
                </div>
            </div>
        </div>
    );
}

export default GoalCreation;