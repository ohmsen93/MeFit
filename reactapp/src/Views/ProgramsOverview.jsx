import { useEffect, useState } from "react"
import { fetchPrograms } from "../API/ProgramAPI"
import { fetchWorkouts } from "../API/WorkoutAPI"
import ProgramSelectionList from "../Components/Program/ProgramSelectionList"
import WorkoutSelectionList from "../Components/Workout/WorkoutSelectionList"

const ProgramsOverview = () => {
    const [state, setState] = useState({
        selectedProgram: null,
        selectedWorkout: null
    })
    const [programs, setPrograms] = useState(null)
    const [workouts, setWorkouts] = useState(null)

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
        if (event.target.checked) setState({...state, selectedProgram: program, selectedWorkout: null})
        else setState({...state, selectedProgram: null})
    }
    const workoutSelected = (event, workout) => {
        console.log(workout)
        if (event.target.checked) setState({...state, selectedWorkout: workout})
        else setState({...state, selectedWorkout: null})
    }

    return (
        <>
        <div className="d-flex flex-column align-items-center hpx-720 p-5">
            <div className="d-flex flex-fill border wp-100 min-h-0">
                <div className="d-flex flex-column text-center wp-100">
                    <ProgramSelectionList type="radio" programs={programs} programSelected={programSelected}/>
                </div>
                <div className="d-flex flex-column text-center wp-100">
                    <WorkoutSelectionList type="radio" workouts={workouts?.filter(w => state.selectedProgram?.workouts.includes(w.id)) || []} workoutSelected={workoutSelected}/>
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
                            {state.selectedProgram.categories?.map(cg => 
                                <p key={cg.id}>{cg.name}</p>
                            )}
                            </>
                            : <p>No categories</p>
                            }
                        </>
                        }
                    </div>
                    <div className="d-flex flex-column justify-content-center hp-100">
                        {state.selectedWorkout !== null &&
                        <>
                            <p>Workout: {state.selectedWorkout.name}</p>
                            <p>Type: {state.selectedWorkout.type}</p>
                        </>
                        }
                    </div>
                </div>
            </div>
        </div>
        </>
    )
}

export default ProgramsOverview