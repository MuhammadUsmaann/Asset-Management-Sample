/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react'

function Table({ headers, setPageNo, pageNo, totalPages, data }: any) {

    const handlePrevious = () => {
        setPageNo((prevPage: number) => Math.max(prevPage - 1, 1));
    };

    const handleNext = () => {
        setPageNo((prevPage: number) => Math.min(prevPage + 1, totalPages));
    };


    return (
        <div className='table-responsive py-5'>
            {/* begin::Table */}
            <table className='table align-middle gs-0 gy-4'>
                {/* begin::Table head */}
                <thead>
                    <tr className='fw-bold text-muted bg-light'>
                        {headers.map((title: string, index: number) => {
                            return (<th className={index === 0 ? 'ps-4 rounded-start' : (headers.length === index + 1 ? ' rounded-end' : '')} key={title}>{title}</th>)
                        })}

                    </tr>
                </thead>
                {/* end::Table head */}
                {/* begin::Table body */}
                <tbody>
                    {
                        data.map((row: string[], index: number) => {
                            return (<>
                                <tr key={index} className=''>
                                    {
                                        row.map((col: string, ind: number) => {
                                            return (<>
                                                <td key={col}>
                                                    <a href='#' className={'text-gray-900 fw-bold text-hover-primary mb-1 fs-6 ' + (ind === 0 ? 'ps-4' : '')}>
                                                        {col}
                                                    </a>
                                                </td>
                                            </>)
                                        })
                                    }
                                </tr >
                            </>)
                        })
                    }
                </tbody>
                {/* end::Table body */}
            </table>
            {/* end::Table */}

            <div className='d-flex flex-stack flex-wrap pt-10'>
                <div className='fs-6 fw-bold text-gray-700'></div>

                <ul className='pagination'>
                    <li className='page-item previous' onClick={handlePrevious}>
                        <a href='#' className='page-link'>
                            <i className='previous'></i>
                        </a>
                    </li>

                    {[...Array(totalPages)].map((_, index) => (
                        <li className={'page-item ' + (pageNo === index + 1 ? " active" : '')}>
                            <a href='#' className='page-link'
                                onClick={() => {
                                    setPageNo(index + 1)
                                }}
                            >
                                {index + 1}
                            </a>
                        </li>

                    ))}
                    <li className='page-item next' onClick={handleNext}>
                        <a href='#' className='page-link'>
                            <i className='next'></i>
                        </a>
                    </li>
                </ul>
            </div>
        </div >
    )
}

export default Table