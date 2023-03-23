import { useEffect, useState } from "react"
import { fetchPrograms, patchProgram, postProgram } from "../API/ProgramAPI"
import { fetchWorkouts } from "../API/WorkoutAPI"
import ProgramSelectionList from "../Components/Program/ProgramSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"
import { categoryCompare } from "../Util/SortHelper"

const ProgramsOverview = props => {
    const [state, setState] = useState({
        selectedProgram: null,
        selectedPWorkout: null,
        selectedWorkout: null
    })
    const [programs, setPrograms] = useState(null)
    const [workouts, setWorkouts] = useState(null)
    const [pWorkouts, setPWorkouts] = useState([])

    useEffect(() => {
        // setPrograms("loading")
        const getPrograms = async () => {
            const ps = await fetchPrograms()
            console.log(ps)
            setPrograms(ps.reverse())
        }
        getPrograms()
    }, [])
    useEffect(() => {
        // setWorkouts("loading")
        const getWorkouts = async () => {
            const ws = await fetchWorkouts()
            console.log(ws)
            setWorkouts(ws.reverse())
        }
        getWorkouts()
    }, [])

    const programSelected = (event, program) => {
        console.log(program)
        if (event.target.checked) {
            setState({...state, selectedProgram: program, selectedPWorkout: null})
            setPWorkouts(workouts?.filter(w => program.workouts.includes(w.id)) || [])
        }
        else setState({...state, selectedProgram: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else setState({...state, selectedWorkout: null})
    }
    const pWorkoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedPWorkout: workout})
        else setState({...state, selectedPWorkout: null})
    }
    const programsSort = () => {
        const ps = [...programs].sort(categoryCompare)
        setPrograms(ps)
    }
    const addPWorkout = () => {
        if (state.selectedWorkout !== null) {
            const index = pWorkouts.indexOf(state.selectedWorkout)
            if (index === -1) // Currently only add if unique
                setPWorkouts([...pWorkouts, state.selectedWorkout])
            else alert("Workout is already in program")
        }
        else alert("Must select a registered workout to add")
    }
    const removePWorkout = () => {
        if (state.selectedPWorkout !== null) {
            const pws = [...pWorkouts]
            const index = pws.indexOf(state.selectedPWorkout)
            if (index > -1) {
                pws.splice(index, 1)
                setPWorkouts(pws)
                setState({...state, selectedPWorkout: null})
            }
            else console.log("ERROR: PWORKOUT NOT FOUND")
        }
        else alert("Must select a program workout to remove")
    }
    const saveProgram = event => {
        event.preventDefault()
        // console.log(event)
        if (pWorkouts.length > 0) {
            if (event.nativeEvent.submitter.name === "save") { // PATCH program
                if (state.selectedProgram !== null) {
                    // console.log(event.target[0].value)
                    const p = {
                        id: state.selectedProgram.id,
                        name: event.target[0].value,
                        workoutIds: pWorkouts.map(w => w.id),
                        categoryIds: state.selectedProgram.categories
                    }
                    patchProgram(state.selectedProgram.id, p)
                        .then(r => {
                            const index = programs.indexOf(state.selectedProgram)
                            if (index > -1) {
                                programs[index] = r
                                setState({...state, selectedProgram: r})
                            }
                        })
                }
                else alert("Must select a program to save")
            }
            else { // POST program
                const p = {
                    name: event.target[0].value,
                    workoutIds: pWorkouts.map(w => w.id),
                    categoryIds: []
                }
                postProgram(p)
                    .then(r => {
                        setPrograms([r, ...programs])
                    })
            }
        }
        else alert("Must add at least 1 workout to program")
    }

    return (
        <>
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <div className="d-flex flex-fill border wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100">
                    Sort by: <button onClick={programsSort} className="btn btn-outline-secondary">Category</button>
                    <ProgramSelectionList type="radio" programs={programs} programSelected={programSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <WorkoutSelectionList type="radio" workouts={pWorkouts} workoutSelected={pWorkoutSelected} k={1}/>
                    {props.contributor && 
                        <>
                        <div className="d-flex">
                            <button onClick={removePWorkout} className="btn btn-outline-secondary wp-100">↓</button>
                            <button onClick={addPWorkout} className="btn btn-outline-secondary wp-100">↑</button>
                        </div>
                        <WorkoutSelectionList type="radio" workouts={workouts} workoutSelected={workoutSelected} k={2}/>
                        </>
                    }
                </div>

                <div className="d-flex flex-column text-center wp-100">
                    <h3>Details</h3>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedProgram !== null &&
                        <>
                            <p>Program: {state.selectedProgram.name}</p>

                            {state.selectedProgram.categories?.length > 0 ?
                            <>
                            <p>Categories:</p>
                            {state.selectedProgram.categories.map(cg => 
                                <p key={cg.id}>{cg.name}</p>
                            )}
                            </>
                            : <p>No categories</p>
                            }
                        </>
                        }
                    </div>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedPWorkout !== null &&
                        <>
                            <p>Workout: {state.selectedPWorkout.name}</p>
                            <p>Type: {state.selectedPWorkout.type}</p>
                        </>
                        }
                    </div>
                    {props.contributor &&
                        <form onSubmit={saveProgram} key="POForm-1" className="d-flex flex-column">
                            <input type="text" defaultValue={state.selectedProgram?.name} placeholder="Program name" title="Letters and spaces only (between 2-40)" pattern="[A-Za-z\s]{2,40}" required/>
                            <input type="submit" name="save" id="save-button-1" className="d-none"/>
                            <label htmlFor={"save-button-1"} className="btn btn-outline-secondary wp-100">Overwrite program</label>
                            <input type="submit" name="create" id="create-button-1" className="d-none"/>
                            <label htmlFor={"create-button-1"} className="btn btn-outline-secondary wp-100">Save as new program</label>
                        </form>
                    }
                </div>
            </div>
        </div>
        </>
    )
}

export default ProgramsOverview