import { useEffect, useState } from "react";
import { fetchPrograms } from "../../API/ProgramAPI";

const ProgramSelectionList = props => {
    const [programs, setPrograms] = useState([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        const getPrograms = async () => {
            const ps = await fetchPrograms()
                .finally(setLoading(false))
            console.log(ps)
            setPrograms(ps)
        }
        getPrograms()
    }, [])

    return (
        <div className="d-flex flex-column flex-fill align-items-center border wp-100 min-h-0 p-2">
            <p>Choose a program:</p>
            {/* <GoalCreationContext.Consumer>
                {(programSelected) => ( */}
                    <div className="d-flex flex-column flex-fill text-center overflow-y-scroll wp-100">
                        {loading && <div className="spinner-border align-self-center" role="status"/>}
                        {programs.map(program => 
                            <div className="d-flex flex-column" key={program.id}>
                                <input onChange={e => props.programSelected(e, program)} type={props.type} name="program-list-radio" id={`program-${program.id}`} className="btn-check"/>
                                <label htmlFor={`program-${program.id}`} className="btn btn-outline-secondary">{program.name}</label>
                            </div>
                        )}
                        {/* <input onChange={e => programSelected(e, {id: 1, name: "Program A"})} type="radio" name="program-list-radio" id="program-radio-1" className="btn-check"/>
                        <label htmlFor="program-radio-1" className="btn btn-outline-secondary">Program A</label>
                        <input onChange={e => programSelected(e, {id: 2, name: "Program B"})} type="radio" name="program-list-radio" id="program-radio-2" className="btn-check"/>
                        <label htmlFor="program-radio-2" className="btn btn-outline-secondary">Program B</label> */}
                    </div>
                {/* )}
            </GoalCreationContext.Consumer> */}
        </div>
    )
}

export default ProgramSelectionList