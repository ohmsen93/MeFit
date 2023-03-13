import { useState } from "react";
import { GoalCreationContext } from "../Context/GoalCreationContext";

const GoalCreation = () => {
    const [state, setState] = useState({
        tab: "program",
        selectedProgram: null,
        selectedWorkouts: [],
        startDate: new Date().toLocaleDateString('fr-ca')
    })

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
    const tabSelected = tab => {
        console.log(tab)
        setState({...state, tab, selectedProgram: null, selectedWorkouts: []})
    }

    return (
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <h1>New goal</h1>
            <div className="d-flex flex-fill border wp-100">
                <div className="d-flex flex-column text-center wp-50 p-2">

                    {/* Goal Form */}
                    <GoalCreationContext.Provider value={state}>
                        <p>GoalForm</p>
                        <div className="hp-100 p-2">
                        <GoalCreationContext.Consumer>
                            {state => (
                                    <>
                                    {state.selectedProgram !== null && <div>{state.selectedProgram.name}</div>}
                                    {state.selectedWorkouts.length > 0 &&
                                        <div className="overflow-y-scroll text-center hp-100">
                                            {state.selectedWorkouts.map(w => <div key={w.id}>{w.name}</div>)}
                                        </div>
                                    }
                                    </>
                            )}
                        </GoalCreationContext.Consumer>
                        </div>
                        <div className="hp-50">
                            <div>
                                <label htmlFor="start-date">Start date:</label>
                                <br/>
                                <input onChange={e => setState({...state, startDate: e.target.value})} type="date" min={new Date().toLocaleDateString('fr-ca')} id="start-date"/>
                            </div>
                            <div>
                                <label htmlFor="end-date">End date:</label>
                                <br/>
                                <input min={state.startDate} type="date" id="start-date"/>
                            </div>
                        </div>
                        <div>
                            <button className="btn btn-outline-secondary">Save goal</button>
                        </div>
                    </GoalCreationContext.Provider>

                </div>
                <div className="d-flex flex-column wp-100">
                    
                    {/* Tabs */}
                    <div>
                        <input onChange={() => tabSelected("program")} type="radio" name="tab-radio" id="program-tab" className="btn-check" defaultChecked/>
                        <label htmlFor="program-tab" className="btn btn-outline-secondary">Programs</label>
                        <input onChange={() => tabSelected("workout")} type="radio" name="tab-radio" id="workout-tab" className="btn-check"/>
                        <label htmlFor="workout-tab" className="btn btn-outline-secondary">Workouts</label>
                        {/* <input onChange={() => setState({...state, tab: "exercise"})} type="radio" name="tab-radio" id="exercise-tab" className="btn-check"/>
                        <label htmlFor="exercise-tab" className="btn btn-outline-secondary">Exercises</label> */}
                    </div>

                    {state.tab === "program" && 
                        <GoalCreationContext.Provider value={programSelected}>
                        {/* ProgramList Component */}
                        <div className="d-flex flex-column flex-fill align-items-center border wp-100 p-2">
                            <p>Choose a program:</p>
                            <GoalCreationContext.Consumer>
                                {programSelected => (
                                    <div className="d-flex flex-column text-center flex-fill text-center overflow-y-scroll wp-100">
                                        <input onChange={e => programSelected(e, {id: 1, name: "Program A"})} type="radio" name="program-list-radio" id="program-radio-1" className="btn-check"/>
                                        <label htmlFor="program-radio-1" className="btn btn-outline-secondary">Program A</label>
                                        <input onChange={e => programSelected(e, {id: 2, name: "Program B"})} type="radio" name="program-list-radio" id="program-radio-2" className="btn-check"/>
                                        <label htmlFor="program-radio-2" className="btn btn-outline-secondary">Program B</label>
                                    </div>
                                )}
                            </GoalCreationContext.Consumer>
                        </div>
                        </GoalCreationContext.Provider>
                    }

                    {state.tab === "workout" && 
                        <GoalCreationContext.Provider value={workoutSelected}>
                            {/* WorkoutList Component */}
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
                        /* ExerciseList Component */
                        <div className="d-flex flex-column flex-fill align-items-center border wp-100 p-2">
                            <p>Choose exercises:</p>
                            <div className="d-flex flex-column text-center flex-fill overflow-y-scroll wp-100">
                                <input type="checkbox" name="exercise-list-checkbox" id="exercise-checkbox-1" className="btn-check"/>
                                <label htmlFor="exercise-checkbox-1" className="btn btn-outline-secondary">Exercise A</label>
                                <input type="checkbox" name="exercise-list-checkbox" id="exercise-checkbox-2" className="btn-check"/>
                                <label htmlFor="exercise-checkbox-2" className="btn btn-outline-secondary">Exercise B</label>
                            </div>
                            <button className="btn btn-outline-secondary">Save workout</button>
                        </div>
                    }
                
                </div>
            </div>
        </div>
    );
}

export default GoalCreation;